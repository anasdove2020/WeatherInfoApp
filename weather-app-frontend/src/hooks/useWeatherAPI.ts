import { useState, useEffect } from 'react';
import axios from 'axios';
import { Country, City, WeatherResponse } from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

export const useWeatherAPI = () => {
  const [countries, setCountries] = useState<Country[]>([]);
  const [cities, setCities] = useState<City[]>([]);
  const [selectedCountry, setSelectedCountry] = useState<string>('');
  const [selectedCity, setSelectedCity] = useState<string>('');
  const [weather, setWeather] = useState<WeatherResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>('');

  const fetchCountries = async () => {
    try {
      setError('');
      const response = await axios.get<Country[]>(`${API_BASE_URL}/countries`);
      setCountries(response.data);
    } catch (err) {
      setError('Failed to load countries. Please make sure the API server is running.');
      console.error('Error fetching countries:', err);
    }
  };

  const fetchCities = async (countryCode: string) => {
    try {
      setError('');
      const response = await axios.get<City[]>(`${API_BASE_URL}/countries/${countryCode}/cities`);
      setCities(response.data);
    } catch (err) {
      setError('Failed to load cities for the selected country.');
      console.error('Error fetching cities:', err);
    }
  };

  const fetchWeather = async (cityName: string) => {
    try {
      setError('');
      setLoading(true);
      const response = await axios.get<WeatherResponse>(`${API_BASE_URL}/weather/${cityName}`);
      setWeather(response.data);
    } catch (err: any) {
      if (err.response?.status === 500) {
        setError('Server error occurred. Please try again later.');
      } else {
        setError('Failed to load weather data. Please try again.');
      }
      console.error('Error fetching weather:', err);
      setWeather(null);
    } finally {
      setLoading(false);
    }
  };

  const handleCountryChange = (countryCode: string) => {
    setSelectedCountry(countryCode);
    setSelectedCity('');
    setWeather(null);
    if (countryCode) {
      fetchCities(countryCode);
    }
  };

  const handleCityChange = (cityName: string) => {
    setSelectedCity(cityName);
    if (cityName) {
      fetchWeather(cityName);
    }
  };

  useEffect(() => {
    fetchCountries();
  }, []);

  return {
    countries,
    cities,
    selectedCountry,
    selectedCity,
    weather,
    loading,
    error,
    handleCountryChange,
    handleCityChange
  };
};