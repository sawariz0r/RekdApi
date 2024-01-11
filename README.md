# RekdApi

RekdApi is a backend service for a game application. It's built with .NET 8.0 and uses Entity Framework Core for data access. The service provides APIs for managing game sessions, users, and authentication via magic links. 

It will be used as a backend for a React Native app in the works.

## Features

- **Game Sessions Management**: The service provides endpoints for creating, retrieving, updating, and deleting game sessions. The game sessions are managed by the `GameSessionsController` in [`RekdApi/Controllers/GameSessionsController.cs`](command:_github.copilot.openRelativePath?%5B%22RekdApi%2FControllers%2FGameSessionsController.cs%22%5D "RekdApi/Controllers/GameSessionsController.cs").

- **User Management**: The service provides endpoints for managing users. The user data includes display name, email, creation date, Expo push tokens, and associated game sessions. User management is handled by the `UsersController` in [`RekdApi/Controllers/UsersController.cs`](command:_github.copilot.openRelativePath?%5B%22RekdApi%2FControllers%2FUsersController.cs%22%5D "RekdApi/Controllers/UsersController.cs").

- **Authentication**: The service uses magic links for authentication. When a user requests a magic link, a token is generated and sent to the user's email. The user can then use this token to authenticate. This is managed by the `MagicLinkController` in [`RekdApi/Controllers/MagicLinkController.cs`](command:_github.copilot.openRelativePath?%5B%22RekdApi%2FControllers%2FMagicLinkController.cs%22%5D "RekdApi/Controllers/MagicLinkController.cs").

- **Notifications**: The service uses the Expo Server SDK to send push notifications to the users. The Expo push tokens are stored in the `User` model.

## Setup: in a Dev Container

To run the project in a Dev Container, you need to have Docker and Visual Studio Code with the Remote - Containers extension installed.

1. Clone the repository:

    ```sh
    git clone <repository-url>
    ```

2. Open the project in Visual Studio Code:

    ```sh
    cd RekdApi
    code .
    ```

3. When Visual Studio Code has opened, a notification will appear asking if you want to reopen the project in a Dev Container. Click "Reopen in Container".

    If the notification doesn't appear, you can press `F1` to open the command palette, then run the `Remote-Containers: Reopen in Container` command.

4. The first time you open the project in a Dev Container, Docker will build the container. This can take a few minutes.

5. Once the container is built, the project will be opened inside the container. You can run the project using the .NET CLI:

    ```sh
    dotnet dev-certs https --trust
    dotnet run --launch-profile https
    ```

The Dev Container uses the same PostgreSQL database as the local setup. The connection string is configured in the `appsettings.json` file.

## Setup "the old way"

To set up the project, you need to have .NET 8.0 SDK installed. Then, you can clone the repository and run the project using the .NET CLI:

```sh
git clone <repository-url>
cd RekdApi
dotnet run
```

The service uses PostgreSQL for data storage. The connection string is configured in the `appsettings.json` file.

## Contributing

I'm fairly new to .NET and this is a learning project, so if you see something strange - please let me know with a PR.

## License

This project is licensed under the MIT License. See the LICENSE file for details.