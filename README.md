## Mini-Trello

### О проекте

Mini-Trello представляет собой платформу для создания и управления досками в стиле Trello. Реализованы следующие функциональности: регистрация и аутентификация с использованием JWT-токенов, создание досок, добавление секций в доски, создание карточек в секциях, возможность перетаскивания при помощи функции drag-and-drop, а также добавление других пользователей на доску. Проект также включает в себя функционал редактирования и удаления всех упомянутых сущностей.

### Технологии

**Backend:**
- PostgreSQL
- ASP.NET
- Entity Framework

**Frontend:**
- React
- React-Redux
- Redux Toolkit (RTK)
- Axios
- CSS, SCSS, Tailwind
- HTML
- React-DnD

**Прочее:**
- JWT-токены
- Docker

### Запуск

1. Установите Docker на вашем компьютере.
2. В корневой папке проекта с файлом `docker-compose.yml` выполните команду
  ```bash
    docker-compose up 
  ```
  в терминале.
5. Откройте браузер и перейдите по адресу [http://localhost:3000/login](http://localhost:3000/login).
### Если вы хотите запустить приложение не в контейнере:
1. Установите postgreSQL на пк
2. Укажите там : порт-5001 user-postgres password-admin
3. создайте там базу данным "trello"
4. Зайдите в файл `./trello_app/Prigram.cs`
5. на 10 строке `var connectionString = builder.Configuration.GetConnectionString("DockerConnection");` измените "DockerConnection" на `DefaultConnection`
6. Зайдите в trello_app.sln и запустите сервер
7. В папке `view` напишите в терминале
   ```bash
   npm i
   ```
   ```bash
     npm run start
   ```
