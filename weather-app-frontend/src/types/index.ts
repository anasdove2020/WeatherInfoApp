export interface Country {
  code: string;
  name: string;
}

export interface City {
  name: string;
  countryCode: string;
  latitude: number;
  longitude: number;
}

export interface WeatherResponse {
  city: string;
  country: string;
  timeUtc: string;
  wind: {
    speedMph: number;
    direction: string;
  };
  visibilityMiles: number;
  skyConditions: string;
  temperatureFahrenheit: number;
  temperatureCelsius: number;
  dewPointFahrenheit: number;
  dewPointCelsius: number;
  relativeHumidity: number;
  pressureInHg: number;
}