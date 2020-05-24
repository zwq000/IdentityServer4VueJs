// export const oidcSettings = JSON.parse(process.env.VUE_APP_OIDC_CONFIG)

// const STS_DOMAIN: string = 'http://localhost:44356';

// const settings: any = {
//     userStore: new WebStorageStateStore({ store: window.localStorage }),
//     authority: STS_DOMAIN,
//     client_id: 'vuejs_code_client',
//     redirect_uri: 'http://localhost:44357/callback.html',
//     automaticSilentRenew: true,
//     silent_redirect_uri: 'http://localhost:44357/silent-renew.html',
//     response_type: 'code',
//     scope: 'openid profile dataEventRecords',
//     post_logout_redirect_uri: 'http://localhost:44357/',
//     filterProtocolClaims: true,
// };

const baseUrl = 'http://localhost:5002';
export const oidcSettings = {
    authority: 'http://localhost:44356',
    clientId: 'vuex-oidc',
    //redirectUri: 'http://localhost:5002/oidc-callback',
    redirectUri: `${baseUrl}/oidc-popup-callback`,
    post_logout_redirect_uri:"'http://localhost:5002/",
    silentRedirectUri: 'http://localhost:5002/oidc-silent-renew.html',
    responseType: 'id_token token',
    scope: 'openid profile dataEventRecords'
}
