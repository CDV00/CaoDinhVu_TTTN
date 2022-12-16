using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly DBContext _context;
        public SliderRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public ISliderQuery BuildQuery()
        {
            return new SliderQuery(_context.Sliders.AsQueryable(), _context);
        }
        public void ChangeOrder(Slider sliderAgent, int orderSliderAffect)
        {
            try
            {
                var sliderAffect = _context.Sliders.FirstOrDefault(s => s.Orders == orderSliderAffect);
                //int orderSliderAgent = sliderAgent.Orders.Value;
                sliderAffect.Orders = sliderAgent.Orders.Value;
                sliderAgent.Orders = orderSliderAffect;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public Slider FindByOrder(int order)
        {
            try
            {
                var slider = _context.Sliders.FirstOrDefault(m => m.Orders == order);
                return slider;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message);
            }
        }
        public int MaxOrder()
        {
            try
            {
                var maxOrder = _context.Sliders.Max(s => s.Orders.Value);
                return maxOrder;
            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception(ex.Message);
            }
        }
    }
}
