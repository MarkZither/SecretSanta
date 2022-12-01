import { EnvService } from '@app/shared/env.service';

export const EnvServiceFactory = () => {
  // Create env
  const env = new EnvService();
  type EnvServiceKey = keyof typeof EnvService;
  const envSrvObjKey = '__env' as EnvServiceKey;
  // Read environment variables from browser window
  type ObjectKey = keyof typeof window;
  const envObjKey = '__env' as ObjectKey;
  const browserWindow = window || {};
  const browserWindowEnv = browserWindow[envObjKey] || {};

  // Assign environment variables from browser window to env
  // In the current implementation, properties from env.js overwrite defaults from the EnvService.
  // If needed, a deep merge can be performed here to merge properties instead of overwriting them.
  for (const key in browserWindowEnv) {
    if (browserWindowEnv.hasOwnProperty(key)) {
      // @ts-ignore
      env[key] = window[envObjKey][key];
    }
  }

  return env;
};

export const EnvServiceProvider = {
  provide: EnvService,
  useFactory: EnvServiceFactory,
  deps: [],
};
