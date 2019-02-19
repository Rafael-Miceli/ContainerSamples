FROM node

COPY package.json ./  

RUN npm set progress=false && npm config set depth 0 && npm cache clean --force
# && npm config set strict-ssl false  
## Storing node modules on a separate layer will prevent unnecessary npm installs at each build RUN npm i && mkdir /ng-app && cp -R ./node_modules ./ng-app  

WORKDIR /ng-app  

COPY . .  RUN $(npm bin)/ng build --prod --build-optimizer

COPY nginx/default.conf /etc/nginx/conf.d/  

RUN rm -rf /usr/share/nginx/html/*  

RUN mkdir /usr/share/nginx/html/app-front

RUN cp -R dist/* /usr/share/nginx/html/app-front

ENTRYPOINT ["npm", "run", "start-con"]