global using BuildingBlocks.CQRS;
global using Ordering.Application.Dtos;
global using static System.Net.Mime.MediaTypeNames;
global using Ordering.Application.Data;
global using Ordering.Domain.Models;
global using Ordering.Domain.ValueObjects;
     global using FluentValidation;
global using Ordering.Application.Exceptions;

global using MediatR;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore;

global using Ordering.Domain.Events;