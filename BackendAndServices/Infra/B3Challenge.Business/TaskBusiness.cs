using AutoMapper;
using Castle.Core.Internal;
using B3Challenge.Business.Interfaces;
using B3Challenge.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using B3Challenge.Repository.Task;
using B3Challenge.Domain.Dtos.Task;

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

            if (dto.Id.HasValue)
                query = query.Where(x => x.Id == dto.Id).AsQueryable();

            if (!dto.Description.IsNullOrEmpty())
                query = query.Where(x => x.Description.Contains(dto.Description)).AsQueryable();

            if (dto.TaskStatusId.HasValue)
                query = query.Where(x => x.TaskStatusId == dto.TaskStatusId).AsQueryable();

            return query.ToList();
        }

        private List<string> ValidateInsertUpdate(Domain.Entities.Task entity)
        {
            var errors = new List<string>();

            try
            {
                //errors.AddRange(ValidateInsertBasicInfo(account));
                //errors.AddRange(ValidateAccountTypes(account));
                //errors.AddRange(ValidateChildren(account));
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            return errors;
        }




        public Domain.Entities.Task GetById(int id)
        {
            return _repository.GetById(id);
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
                _repository.Delete(entity.Id);
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