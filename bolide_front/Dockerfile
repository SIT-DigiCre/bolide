FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /app
COPY . .
RUN dotnet publish -c Release ./bolide_front.csproj

FROM nginx:1.19
COPY --from=builder /app/bin/Release/net5.0/publish/wwwroot/ /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/
COPY nginx/conf.d /etc/nginx/conf.d/
