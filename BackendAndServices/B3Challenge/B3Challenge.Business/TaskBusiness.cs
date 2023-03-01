using AutoMapper;
using Castle.Core.Internal;
using B3ChallengeBusiness.Interfaces;
using B3ChallengeDomain;
using B3ChallengeRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;

namespace B3ChallengeBusiness
{
    public class TaskBusiness : ITaskBusiness
    {

        private readonly IGenericRepository<B3ChallengeDomain.Task> _repository;

        public TaskBusiness()
        {

        }
        public TaskBusiness(IGenericRepository<B3ChallengeDomain.Task> repository)
        {
            _repository = repository;
        }

        private List<string> ValidateInsertUpdate(B3ChallengeDomain.Task entity)
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




        public B3ChallengeDomain.Task GetById(int id)
        {
            return _repository.GetById(id);
        }

        public OperationResult Insert(B3ChallengeDomain.Task entity, bool autoSave = true)
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

        public OperationResult Update(B3ChallengeDomain.Task entity, bool autoSave = true)
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

        public List<B3ChallengeDomain.Task> GetAll()
        {

            var all = _repository.GetAll()
                .OrderBy(x => x.Description)
                .ToList();

            return all;
        }
    }
}