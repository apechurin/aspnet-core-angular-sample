# Sample ASP.NET Core + Angular project

## Требования

- .NET 8
- Angular 17
- PostgreSQL

## Создание схемы БД

Имя схемы - `sample`.

Для создания использовать скрипт `aspnet-core/Database/migrations_20261105_114938.sql`.

## Запуск Development версии

Инструкция здесь и далее для Linux.

- В окне терминала перейти в каталог `aspnet-core/Sample.Host`.
- Выполнить
```
$ cp appsettings.Development.json.sample appsettings.Development.json
```
- В файле `appsettings.Development.json` настроить соединение с БД.
- Запустить команду `dotnet run`.
- В другом окне терминала перейти в каталог `angular`.
- Выполнить
```
$ npm install
$ npm run start
```
- В браузере открыть адрес `http://localhost:4200`.

## Сборка Production версии

- В окне терминала перейти в каталог `aspnet-core/Sample.Host`.
- В файле `publish.sh` оставить раскомментированным одно значение переменной RID (для Windows или Linux сборки).
- Выполнить `publish.sh`.
- В каталоге `aspnet-core/Sample.Host/publish` будет собранная версия.

## Запуск Production версии

- В окне терминала перейти в каталог `aspnet-core/Sample.Host/publish`.
- Выполнить
```
$ cp appsettings.json.sample appsettings.json
```
- В файле `appsettings.json` настроить соединение с БД.
- Запустить команду `./Sample.Host`.
- В браузере открыть адрес `https://localhost:5001`.
- Подтвердить небезопасную страницу.
