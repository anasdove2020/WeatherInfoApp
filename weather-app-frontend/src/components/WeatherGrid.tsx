import React from 'react';
import { WeatherResponse } from '../types';

interface WeatherGridProps {
  weather: WeatherResponse;
}

const WeatherGrid: React.FC<WeatherGridProps> = ({ weather }) => {
  return (
    <div className="weather-grid">
      <div className="weather-item temperature">
        <h4>TEMPERATURE</h4>
        <p>{weather.temperatureCelsius}°C</p>
        <p>{weather.temperatureFahrenheit}°F</p>
      </div>

      <div className="weather-item">
        <h4>WIND</h4>
        <p>{weather.wind.speedMph} mph</p>
        <p>{weather.wind.direction}</p>
      </div>

      <div className="weather-item">
        <h4>VISIBILITY</h4>
        <p>{weather.visibilityMiles} miles</p>
      </div>

      <div className="weather-item">
        <h4>HUMIDITY</h4>
        <p>{weather.relativeHumidity}%</p>
      </div>

      <div className="weather-item">
        <h4>PRESSURE</h4>
        <p>{weather.pressureInHg} inHg</p>
      </div>

      <div className="weather-item">
        <h4>DEW POINT</h4>
        <p>{weather.dewPointCelsius}°C</p>
        <p>{weather.dewPointFahrenheit}°F</p>
      </div>
    </div>
  );
};

export default WeatherGrid;