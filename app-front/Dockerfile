FROM node as build

COPY package.json ./  

RUN npm set progress=false && npm config set depth 0 && npm cache clean --force 

# && npm config set strict-ssl false  
## Storing node modules on a separate layer will prevent unnecessary npm installs at each build 

RUN npm i && mkdir /ng-app && cp -R ./node_modules ./ng-app  

WORKDIR /ng-app  

COPY . .  

RUN $(npm bin)/ng build --prod --build-optimizer

# ENTRYPOINT [ "npm", "start" ]

FROM nginx 

RUN rm -rf /usr/share/nginx/html/*

WORKDIR /usr/share/nginx/html/

COPY startup.sh .

COPY --from=build /ng-app/dist/app-front .