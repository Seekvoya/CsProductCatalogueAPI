# CsProductCatalogueAPI

# CsProductCatalogueAPI

## Описание

CsProductCatalogueAPI — это простой веб-сервис, который предоставляет RESTful API для управления товарами и категориями товаров. Приложение разработано с использованием ASP.NET Core и Entity Framework Core, а в качестве базы данных используется SQLite.

## Используемые технологии

- **C#** + **ASP.NET Core**
- **Entity Framework Core**
- **SQLite**

## Требования для запуска

- .NET SDK 7.0 или новее (последняя стабильная версия .NET)
- SQLite

## Установка

1. **Клонируйте репозиторий:**

   ```bash
   git clone https://github.com/ваш_пользователь/CsProductCatalogueAPI.git
   cd CsProductCatalogueAPI

## Инициализация

dotnet restore

## Миграции

dotnet ef migrations add InitialCreate
dotnet ef database update

## Запуск приложения

dotnet run



## API Документация

Категория товара (ProductCategory)
GET /api/productcategories: Получить все категории товаров.

GET /api/productcategories/{id}: Получить категорию по id.

POST /api/productcategories: Создать новую категорию.

PUT /api/productcategories/{id}: Обновить существующую категорию.

DELETE /api/productcategories/{id}: Удалить категорию по id.

Товар (Product)
GET /api/products: Получить все товары.

GET /api/products/{id}: Получить товар по id.

POST /api/products: Создать новый товар.

PUT /api/products/{id}: Обновить существующий товар.

DELETE /api/products/{id}: Удалить товар по id.


