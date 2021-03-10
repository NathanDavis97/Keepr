dotnet publish -c Release
docker build -t keepr ./bin/Release/net5.0/publish
docker tag keepr registry.heroku.com/vaultkeeper/web
docker push registry.heroku.com/vaultkeeper/web
heroku container:release web -a vaultkeeper
echo press any key
read end