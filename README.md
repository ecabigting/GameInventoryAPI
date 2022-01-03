
<p align="center">
    <img src="./gi-clean.png">
</p>

# Game Inventory API

![.net](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)

A simple game inventory API build with .net 5 with Repository Pattern

Spin up a docker image for MongoDB
```bash
sudo docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME= -e MONGO_INITDB_ROOT_PASSWORD= mongo
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

Running the building the docker image `-t` speficy a tag version 1 and `.` to specify as the current directory
`note: cannot use caps on tag `
```bash
docker build -t gameinventory:v1 .
```