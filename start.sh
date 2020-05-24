#!/bin/bash

cd StsServer
dotnet watch run &
cd ..

cd ResourceServer
dotnet watch run &
cd ..

cd VueJsOidcClient/vue-js-oidc-client/
npm run serve &

