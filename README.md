# microservicesample01
Simple microservice using mongo db as backend

you need to install docker desktop to run the mongo db

after installing the docker desktop, install/download the mongo db image

by running on your command prompt:

docker run -d --rm --name Mongo -p 27017:27017 -v mongodbdata:/data/db mongo
