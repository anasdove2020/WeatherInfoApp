import React from 'react';
import { Country } from '../types';

interface CountrySelectorProps {
  countries: Country[];
  selectedCountry: string;
  onCountryChange: (countryCode: string) => void;
}

const CountrySelector: React.FC<CountrySelectorProps> = ({
  countries,
  selectedCountry,
  onCountryChange
}) => {
  return (
    <div className="form-group">
      <label htmlFor="country-select">Country</label>
      <select
        id="country-select"
        value={selectedCountry}
        onChange={(e) => onCountryChange(e.target.value)}
      >
        <option value="">-- Choose a Country --</option>
        {countries.map((country) => (
          <option key={country.code} value={country.code}>
            {country.name}
          </option>
        ))}
      </select>
    </div>
  );
};

export default CountrySelector;