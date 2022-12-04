# SecretSanta

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 11.2.3.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artefacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

## Setting up VS Code debugging

It seems like VS Code will generate launch.json with various configurations, such as `Launch Edge against localhost` which will debug a running application, but doesn't have a combined build/run and debug. Adding a preLaunchTask and related tasks.json can achieve that as described in [Debug angular like PRO](https://medium.com/@iReal_Nirmal/debug-angular-like-pro-with-visual-studio-code-54522ca923f1).

Using the exact task definition in the article caused errors like

``` node
Error: the pattern property tsc is not a valid pattern variable name.
Error: the description can't be converted into a problem matcher:
```

I had success with the following reduced task definition

``` json
{
 "version": "2.0.0",
 "tasks": [
  {
   "type": "npm",
   "script": "start",
   "isBackground":true,
   "presentation":{
    "focus":true,
    "panel":"dedicated"
   },
   "problemMatcher": "$tsc",
   "label": "npm: start",
   "detail": "ng serve",
   "group": {
    "kind": "build",
    "isDefault": true
   }
  }
 ]
}
```

## Project Structure

The project structure is based on [https://angular.io/guide/styleguide#folders-by-feature-structure](https://angular.io/guide/styleguide#folders-by-feature-structure) best practices gleaned from:

* <https://stackoverflow.com/questions/56938106/angular-project-structure-for-enterprise-projects>
* <https://medium.com/@shijin_nath/angular-right-file-structure-and-best-practices-that-help-to-scale-2020-52ce8d967df5>
* <https://blogs.halodoc.io/angular-best-practices/>

## Logging

Basic logging service added based on [Logging in Angular Application](https://thesiddharthraghuvanshi.medium.com/logging-in-angular-application-angular-logger-service-8bc90096dcf6) to add a logging service with colour coded logging to console, to be extended in future to log to the backend or hosted service like logz.io.

## Using the tsconfig paths

To avoid imports with `../` and worse and to make everything ready to split out to libraries use  [paths](https://stackoverflow.com/questions/50679031/tsconfig-paths-not-working) to turn
`'../shared/logger.service'` into `'@app/shared/logger.service'`.

## AuthGuard

Prevent access to profile to unauthenticated users with and AuthGuard <https://kirjai.com/dynamic-guard-redirects-angular/>

## Environment Service

So that this app can be built once and deployed to multiple environments it uses the environment service to load the apiurl at runtime rather than define it at build time.

A solution I have used before is explained by [Jurgen Van de Moere](https://www.jvandemo.com/how-to-use-environment-variables-to-configure-your-angular-application-without-a-rebuild/), recently this has started to cause errors like

``` bash
Element implicitly has an 'any' type because expression of type 'string' can't be used to index type 'EnvService'.
  No index signature with a parameter of type 'string' was found on type 'EnvService'.
```

This can be fixed with `// @ts-ignore` or moving to [accessing object property dynamically in TypeScript](https://bobbyhadz.com/blog/typescript-access-object-property-dynamically)

Taking it a bit further you can use `envsubst` to [Dynamically set Angular Environment Variables in Docker](https://pumpingco.de/blog/environment-variables-angular-docker/).

## Other Resources
[12 Factor app](https://github.com/rfreedman/angular-configuration-service)
[Angular Book](https://angular-book.dev/)