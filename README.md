# WebShellTest

<h2>Описание</h2>

Тестовое задание.
Веб-приложение для запуска cmd команд для windows.
История запросов пишется в БД на MsSql
Взаимодействие UI на React и сервера происходит в режиме реального времени через SignalR

<h2>Как запустить:</h2>

Из директории ./WebShell/WebShell/ вызвать командную строку
Выполнить:

```
dotnet build WebShell.csproj
dotnet dev-certs https --trust
dotnet run --project WebShell.csproj --property:Configuration=Release
```

Из директории ./WebShell/frontend/web-shell/ вызвать командную строку
Выполнить:

```
npm start
```

Доступ к API:
https://localhost:5001/index.html

Доступ к UI:
https://localhost:3000

<h2>Известные проблемы: </h2>

* При контейнеризации в Docker из-за отсутствия подписанных сертификатов SSL не поднимается Kestrel. Соответственно, все запросы туда падают :(
* Костыль в POST методе
* На фронте в начале и конце списка команд необходимо нажимать клавиши UpArrow и DownArrow дважды 
* Неочевидная файловая структура
