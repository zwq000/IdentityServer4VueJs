<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <div class="home">
      <p v-if="isLoggedIn">User: {{ username }}</p>
      <button class="btn" @click="login" v-if="!isLoggedIn">Login</button>
      <button class="btn" @click="logout" v-if="isLoggedIn">Logout</button>
      <button class="btn" @click="getProtectedApiData" v-if="isLoggedIn">Get API data</button>
      <button class="btn" @click="getIdentityInfo" v-if="isLoggedIn">Get Identity Info</button>
    </div>

    <div v-if="dataEventRecordsItems && dataEventRecordsItems.length">
      <div v-for="(item,i) of dataEventRecordsItems" :key="i">
        <p>
          <em>Id:</em>
          {{item.id}}
          <em>Details:</em>
          {{item.name}} -
          {{item.description}} - {{item.timestamp}}
        </p>
      </div>
      <br />
    </div>

    <div v-if="identityInfo">{{identityInfo}}</div>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import AuthService from '@/services/auth.service';

import axios from 'axios';

const auth = new AuthService();

@Component({
  components: {},
})
export default class Home extends Vue {
  public currentUser: string | undefined = '';
  public identityInfo: object = {};
  public accessTokenExpired: boolean | undefined = false;
  public isLoggedIn: boolean = false;

  public dataEventRecordsItems: [] = [];

  get username(): string {
    return this.currentUser || 'No User';
  }

  public login() {
    auth.login();
  }

  public logout() {
    auth.logout();
  }

  public mounted() {
    auth.getUser().then((user) => {
      if (user) {
        this.currentUser = user.profile.name;
        this.accessTokenExpired = user.expired;
        this.isLoggedIn = user !== null && !user.expired;
      }
    });
  }

  public getProtectedApiData() {
    auth.getAccessToken().then((userToken: string) => {
      axios.defaults.headers.common.Authorization = `Bearer ${userToken}`;
      axios
        .get('http://localhost:44355/api/DataEventRecords/')
        .then((response: any) => {
          this.dataEventRecordsItems = response.data;
        })
        .catch((error: any) => {
          alert(error);
        });
    });
  }

  public getIdentityInfo() {
    auth.getAccessToken().then((userToken: string) => {
      axios.defaults.headers.common.Authorization = `Bearer ${userToken}`;
      axios
        .get('http://localhost:44355/identity')
        .then((response: any) => {
          this.identityInfo = response.data;
        })
        .catch((error: any) => {
          alert(error);
        });
    });
  }
}
</script>
<style>
.btn {
  color: #42b983;
  font-weight: bold;
  background-color: #007bff;
  border-color: #007bff;
  display: inline-block;
  font-weight: 400;
  text-align: center;
  vertical-align: middle;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
  background-color: transparent;
  border: 1px solid #42b983;
  padding: 0.375rem 0.75rem;
  margin: 10px;
  font-size: 1rem;
  line-height: 1.5;
  border-radius: 0.25rem;
  transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out,
    border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}
</style>
