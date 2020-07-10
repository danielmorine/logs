FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

ENV ASPNETCORE_URLS http://+:5000

EXPOSE 5000
EXPOSE 5001

RUN mkdir temp && chmod 777 -R temp
COPY . temp/
RUN cd temp/ && dotnet restore && dotnet build && dotnet test
RUN cd temp/ && mkdir dist && chmod 777 -R dist/
RUN cd temp/ && dotnet publish -o dist/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/temp/dist ./
ENTRYPOINT ["dotnet", "reg.dll"]
