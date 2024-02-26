﻿using Application.Features.Titles;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Results.Unions;

namespace Web.Interfaces;

public interface ITitleService : IWebService<Title, CreateTitle.Request, UpdateTitle.Request>
{
	Task<GetResult<Title>> FindBySlugAsync(string slug);

	Task<GetAllResult<Title>> FindAllPopularAsync(int count = 10, int page = 0);

	Task<GetAllResult<Title>> FindAllByTypeAsync(TitleType type, int count = 10, int page = 0);

	Task<GetAllResult<Title>> FindAllByNameAsync(string name, int count = 10, int page = 0);

	Task<GetAllResult<Title>> FindAllByGenreAsync(string genre, int count = 10, int page = 0);

	Task<GetAllResult<Title>> FindAllByGenresAsync(List<string> genres, int count = 10, int page = 0);

	Task<GetAllResult<Title>> FilterAllAsync(List<string> genres, TitleType? type = null, string? name = null, TitleSorting sorting = TitleSorting.None, int count = 10, int page = 0);

	Task<int> CountByTypeAsync(TitleType type);

	Task<int> CountByNameAsync(string name);

	Task<int> CountByGenreAsync(string genre);

	Task<int> CountByGenresAsync(List<string> genres);

	Task<int> CountByFilter(List<string> genres, TitleType? type = null, string? name = null, TitleSorting sorting = TitleSorting.None);
}