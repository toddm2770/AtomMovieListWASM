using AtomMovieListWASM.Models;
using System.Net.Http.Json;

namespace AtomMovieListWASM.Services
{
    public class TMDBService
    {
        private readonly HttpClient _httpClient;
        public TMDBService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            try
            {
                string? TMDBKey = configuration["TMDBKey"] ?? throw new Exception("TMDB API Key was not found");

                _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", TMDBKey);
                _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
                _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MovieResponse?> GetNowPlayingMoviesAsync()
        {
            string? url = "movie/now_playing?language=en-US&page=1";

            MovieResponse? response = await _httpClient.GetFromJsonAsync<MovieResponse>(url);

            return response;
        }

		public async Task<MovieResponse?> GetPopularMoviesAsync()
		{
			string? url = "movie/popular?language=en-US&page=1";

			MovieResponse? response = await _httpClient.GetFromJsonAsync<MovieResponse>(url);

			return response;
		}

		public async Task<MovieDetails?> GetMovieDetailsAsync(int movieId)
        {
            return await _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{movieId}");
        }

    }
}
