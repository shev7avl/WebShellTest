1

# WebShellTest

2

​

3

<h2>Описание</h2>

4

Тестовое задание.

5

Веб-приложение для запуска cmd команд для windows.

6

​

7

История запросов пишется в БД на MsSql

8

Взаимодействие UI на React и сервера происходит в режиме реального времени через SignalR

9

​

10

<h2>Как запустить:</h2>

11

​

12

Из директории ./WebShell/WebShell/ вызвать командную строку

13

Выполнить:

14

​

15

```

16

dotnet build WebShell.csproj

17

dotnet dev-certs https --trust

18

dotnet run --project WebShell.csproj --property:Configuration=Release

19

```

20

​

21

Из директории ./WebShell/frontend/web-shell/ вызвать командную строку

22

Выполнить:

23

​

24

```

25

npm start

26

```

27

​

28

Доступ к API:

29

https://localhost:5001/index.html

30

​

31

Доступ к UI:

32

https://localhost:3000

33

​

34

<h2>Известные проблемы: </h2>

35

 - При контейнеризации в Docker из-за отсутствия подписанных сертификатов SSL не поднимается Kestrel. Соответственно, все запросы туда падают :(

36

 - Костыль в POST методе

37

 - На фронте в начале и конце списка команд необходимо нажимать клавиши UpArrow и DownArrow дважды 

38

​