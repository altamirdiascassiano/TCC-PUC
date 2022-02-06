FROM mcr.microsoft.com/dotnet/aspnet:5.0

ENV TZ=America/Sao_Paulo
ENV DISABLE_KERNEL_MEMORY_ACCOUNTING=true
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

USER root

COPY / App/
  
WORKDIR /App
ENTRYPOINT ["dotnet", "gisa.mic.backend.dll"]