import { useWeatherAPI } from './hooks/useWeatherAPI';
import Header from './components/Header';
import CountrySelector from './components/CountrySelector';
import CitySelector from './components/CitySelector';
import LoadingSpinner from './components/LoadingSpinner';
import ErrorMessage from './components/ErrorMessage';
import WeatherDisplay from './components/WeatherDisplay';

function App() {
  const {
    countries,
    cities,
    selectedCountry,
    selectedCity,
    weather,
    loading,
    error,
    handleCountryChange,
    handleCityChange
  } = useWeatherAPI();

  return (
    <div className="container">
      <Header />
      
      <div className="card">
        <CountrySelector
          countries={countries}
          selectedCountry={selectedCountry}
          onCountryChange={handleCountryChange}
        />

        <CitySelector
          cities={cities}
          selectedCity={selectedCity}
          onCityChange={handleCityChange}
          selectedCountry={selectedCountry}
        />

        {error && <ErrorMessage message={error} />}
        {loading && <LoadingSpinner />}
        {weather && !loading && <WeatherDisplay weather={weather} />}
      </div>
    </div>
  );
}

export default App;