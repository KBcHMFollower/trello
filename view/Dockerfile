FROM node

WORKDIR /app

COPY ./view/package*.json ./

RUN npm install

COPY ./view .

CMD ["npm", "run", "start"]