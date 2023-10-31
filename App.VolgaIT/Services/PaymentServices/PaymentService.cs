using Infrastructure.VolgaIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.Services.PaymentServices
{
    public class PaymentService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddMoney(string id)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find user by {id}");

            user.Balance += 250000;

            _unitOfWork.UserRepository.UpdateEntity(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddMoney(string id, string login)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find user by {id}");

            if(user.Login != login)
                throw new ArgumentException("Can`t add money to another user");

            user.Balance += 250000;

            _unitOfWork.UserRepository.UpdateEntity(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
