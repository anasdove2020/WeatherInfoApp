import React from 'react';
import { WeatherResponse } from '../types';
import WeatherGrid from './WeatherGrid';

interface WeatherDisplayProps {
  weather: WeatherResponse;
}

const WeatherDisplay: React.FC<WeatherDisplayProps> = ({ weather }) => {
  const formatDateTime = (utcString: string) => {
    try {
      const date = new Date(utcString);
      return date.toLocaleString('en-US', {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        timeZoneName: 'short'
      });
    } catch {
      return utcString;
    }
  };

  return (
    <div className="weather-info">
      <div className="weather-header">
        <h2>{weather.city}, {weather.country}</h2>
        <p>{formatDateTime(weather.timeUtc)}</p>
        <p>Sky Conditions: {weather.skyConditions}</p>
      </div>

      <WeatherGrid weather={weather} />
    </div>
  );
};

export default WeatherDisplay;