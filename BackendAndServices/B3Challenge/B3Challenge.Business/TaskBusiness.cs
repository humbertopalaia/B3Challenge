using AutoMapper;
using Castle.Core.Internal;
using B3Challenge.Business.Interfaces;
using B3Challenge.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using B3Challenge.Repository.Task;

namespace B3Challenge.Business
{
    public class TaskBusiness : ITaskBusiness
    {

        private readonly TaskRepository _repository;

        public TaskBusiness()
        {

        }
        public TaskBusiness(TaskRepository repository)
        {
            _repository = repository;
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

        public OperationResult Insert(Domain.Entities.Task entity, bool autoSave = true)
        {
            var result = new OperationResult();

            var validations = ValidateInsertUpdate(entity);


            if (validations.Count > 0)
                result = new OperationResult { Success = false, Errors = validations };
            else
            {

                _repository.Insert(entity);
                result.Success = true;
            }

            return result;
        }

        public OperationResult Update(Domain.Entities.Task entity, bool autoSave = true)
        {
            var result = new OperationResult();

            var validations = ValidateInsertUpdate(entity);


            if (validations.Count > 0)
                result = new OperationResult { Success = false, Errors = validations };
            else
            {
                _repository.Update(entity);
                result = new OperationResult { Success = true };

            }

            return result;

        }

        public OperationResult Delete(int id)
        {
            var entity = _repository.Get(x => x.Id == id).FirstOrDefault();

            if (entity != null)
                _repository.Delete(entity.Id);

            return new OperationResult { Success = true };
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