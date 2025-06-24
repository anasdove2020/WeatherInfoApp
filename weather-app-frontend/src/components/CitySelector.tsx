import React from 'react';
import { City } from '../types';

interface CitySelectorProps {
  selectedCountry: string;
  cities: City[];
  selectedCity: string;
  onCityChange: (cityName: string) => void;
}

const CitySelector: React.FC<CitySelectorProps> = ({
  selectedCountry,
  cities,
  selectedCity,
  onCityChange
}) => {
  return (
    <div className="form-group">
      <label htmlFor="city-select">City</label>
      <select
        id="city-select"
        value={selectedCity}
        onChange={(e) => onCityChange(e.target.value)}
        disabled={!selectedCountry || cities.length === 0}
      >
        <option value="">-- Choose a City --</option>
        {cities.map((city) => (
          <option key={city.name} value={city.name}>
            {city.name}
          </option>
        ))}
      </select>
    </div>
  );
};

export default CitySelector;