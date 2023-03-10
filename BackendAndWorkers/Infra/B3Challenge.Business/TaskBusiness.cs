using AutoMapper;
using Castle.Core.Internal;
using B3Challenge.Business.Interfaces;
using B3Challenge.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using B3Challenge.Repository.Task;
using B3Challenge.Domain.Dtos.Task;
using System.Globalization;

namespace B3Challenge.Business
{
    public class TaskBusiness : ITaskBusiness
    {

        private readonly TaskRepository _repository;


        public TaskBusiness(TaskRepository repository)
        {
            _repository = repository;
        }


        public IList<Domain.Entities.Task> Filter(TaskFilterDto dto)
        {
            var query = _repository.GetAll();

            query = query.Include(x => x.TaskStatus).AsQueryable();

            if (dto.Id.HasValue)
                query = query.Where(x => x.Id == dto.Id).Include(x => x.TaskStatus).AsQueryable();

            if (!dto.Description.IsNullOrEmpty())
                query = query.Where(x => x.Description.Contains(dto.Description)).Include(x => x.TaskStatus).AsQueryable();

            if (dto.TaskStatusId.HasValue)
                query = query.Where(x => x.TaskStatusId == dto.TaskStatusId).Include(x => x.TaskStatus).AsQueryable();


            DateTime? dtFilterMin = null;
            DateTime? dtFilterMax = null;

            if (dto.Date.HasValue)
            {
                dtFilterMin = new DateTime(dto.Date.Value.Year, dto.Date.Value.Month, dto.Date.Value.Day, 0, 0, 0);
                dtFilterMax = new DateTime(dto.Date.Value.Year, dto.Date.Value.Month, dto.Date.Value.Day, 23, 59, 59);
            }

            if (dto.Date.HasValue)
                query = query.Where(x => x.Date >= dtFilterMin.Value && x.Date <= dtFilterMax.Value).AsQueryable();

            return query.ToList();
        }


        public Domain.Entities.Task GetById(int id)
        {
            return _repository.Get(x =>x.Id == id).FirstOrDefault();
        }

        public void Insert(Domain.Entities.Task entity, bool autoSave = true)
        {
            _repository.Insert(entity);
        }

        public void Update(Domain.Entities.Task entity, bool autoSave = true)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _repository.Get(x => x.Id == id).FirstOrDefault();

            if (entity != null)
                _repository.Delete(entity);
        }

        public List<Domain.Entities.Task> GetAll()
        {

            var all = _repository.GetAll()
                .OrderBy(x => x.Description)
                .ToList();

            return all;
        }
    }
}