using Entities.Models;
using Query.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ISliderRepository : IRepository<Slider>
    {
        ISliderQuery BuildQuery();
        void ChangeOrder(Slider sliderAgent, int orderSliderAffect);
        Slider FindByOrder(int order);
        int MaxOrder();
    }
}
