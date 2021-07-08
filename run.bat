rem "docker build -t api:v1 ." slouzi pro vytvoreni docker img na vašem zarizeni, api je nazev 
rem "docker run -it --rm -p 8080:80 api:v1" slouzi k vytvoreni a spusteni docker contejneru, --rm po ukonceni dany kontejner smaze, -p 8080:80 znaci port na kterem api bezi
rem  http://localhost:8080/customer

docker build -t api:v1 . 
docker run -it --rm -p 8080:80 api:v1