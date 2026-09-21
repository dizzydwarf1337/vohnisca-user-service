# Agents — vohnisca-user-service

## Identity
Профили пользователей, друзья и уведомления. Единственный .NET-сервис с собственной БД (EF Core +
PostgreSQL). Вход — JSON-RPC от gateway; профиль создаётся по событию RabbitMQ после регистрации.

## Stack
- C# **.NET 10** (не 9 — не переноси API вслепую из gateway/mail).
- Четыре слоя: **Domain / Application / Persistence / API**.
- JSON-RPC сервер — **EdjCase.JsonRpc.Router**; **EF Core + PostgreSQL**; MassTransit consumer; JWT (JwtBearer); Serilog.

## Read order
1. В мета-репо: `.claude/rules/dotnet-preflight.md`.
2. Этот файл.
3. `Persistence/Database/VohniscaDbContext.cs` — перед правками схемы/сущностей.

## Verification
- Сборка: `dotnet build vohnisca-user-service/vohnisca-user-service.sln`
- Тесты: `dotnet test vohnisca-user-service/vohnisca-user-service.sln` (точечно `--filter <TestName>`).

## Эталон
- Команда: `vohnisca-user-service/Application/Commands/User/Users/UpdateUserData/`.
- Контроллер: `vohnisca-user-service/api/Controllers/User/UserController.cs`.
- DbContext: `vohnisca-user-service/Persistence/Database/VohniscaDbContext.cs`.

## Локальные конвенции
- **Миграции EF Core:** `dotnet ef migrations add <Name> --project Persistence --startup-project api`,
  применение `dotnet ef database update --project Persistence --startup-project api`. При старте
  контейнера миграции прогоняются автоматически (`db.Database.Migrate()`).
- Request-иерархия `PublicRequest`/`UserRequest`/`AdminRequest` от `AuthorizedRequest`; behaviors:
  Validation → UserAuthorization/AdminAuthorization → Exception.
- Методы: User (GetMe, UpdateUserData, GetUser, DeleteProfilePicture), FriendRequests (Send/Accept/
  Reject/Cancel/Delete), Friends (GetFriends, DeleteFriend).
- Картинки профиля — через `IBlobStorage` (blob-хранилище), не в БД.
- Строка подключения к PostgreSQL — из env docker-compose (`ConnectionStrings__DefaultConnection`), не хардкодь.

## Коммит-граница
Репозиторий `dizzydwarf1337/vohnisca-user-service`, дефолтная ветка `main`. Коммиты — сюда.
Ветка текущей работы: `core/implement-user-service-workspace`.
