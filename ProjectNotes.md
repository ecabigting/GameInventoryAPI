# Project notes #

## Here are some notes for building a docker image for the MongoDB and the .net 5 API ##


Spin up a docker image for MongoDB
```bash
sudo docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=value --network=gameapinetwork mongo
```

Initialize dotnet Secret Manager
```bash
dotnet user-secrets init
```

Set dotnet Secret password 

``NOTE: DO NOT USE '@' as part of any value for MongoDB connection string. It is a reserved character``
```bash
dotnet user-secrets set DBSettings:Password value
```

Running build `Dockerfile` in the project, the docker image `-t` speficy a tag version 1 and `.` to specify as the current directory
`note: cannot use caps on tag `
```bash
docker build -t gameinventory:v1 .
```

Check if the image `gameinventory:v1` is available
```bash
docker images
```

Create a docker network
```bash
docker network create gameapinetwork
```

Running the `gameinventory v1` image on docker.
 - `-it` is to let the docker image interact with the terminal, to see the logs from that container in the terminal. 
 - `--rm` automatically deletes the container when we stop it.
 - `-p` port that will be mapped from the local machine into the container.
 - `-e` defining an environment varibale for aspnet to use value from appsettings.
 - `--network` join the container to sa specified network 

`noted: for aspnet and docker 5 images, the base images itself overwrites the port where the app executes, at least for http which is port 80`
```bash
docker run -it --rm -p 8080:80 -e DBSettings:Host=mongo -e DBSettings:Password=value --network gameapinetwork gameinventory:v1
```

Publishing an image to `docker hub`
```bash
docker push <username>/gameinventory:v1
```