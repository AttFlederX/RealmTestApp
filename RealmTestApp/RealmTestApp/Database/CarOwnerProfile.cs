using AutoMapper;
using RealmTestApp.Models.Database;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmTestApp.Database
{
    public class CarOwnerProfile : Profile
    {
        public CarOwnerProfile() {
            //CreateMap<Car, CarDTO>()
            //    .ForMember( dst => dst.Owner, opt => opt.Ignore() )
            //    .ForMember( dst => dst.FullModelName, opt => opt.Ignore() );

            //CreateMap<CarDTO, Car>();
            //    //.ForMember( dst => dst.CarId, opt => opt.Ignore() )
            //    //.ForMember( dst => dst.Owner, opt => opt.Ignore() )
            //    //.ForMember( dst => dst.OwnerId, opt => opt.MapFrom( src => src.Owner.OwnerId ) );


            //CreateMap<Owner, OwnerDTO>()
            ////.ConstructUsing( src => new OwnerDTO( src.OwnerId ) )
            //.ForMember( dst => dst.Cars, opt => opt.MapFrom( src => src.Cars.Select( c => Mapper.Map<Car, CarDTO>( c ) ) ) )
            //.AfterMap( ( src, dst ) => { // avoid circular reference
            //    foreach (var c in dst.Cars) {
            //        c.Owner = dst;
            //    }
            //} );

            //CreateMap<OwnerDTO, Owner>()
            //    //.ForMember( dst => dst.OwnerId, opt => opt.Ignore() )
            //    .ForMember( dst => dst.Cars, opt => opt.MapFrom( src => src.Cars.Select( c => Mapper.Map<CarDTO, Car>( c ) ) ) )
            //    .AfterMap( ( src, dst ) => { // avoid circular reference
            //        foreach (var c in dst.Cars) {
            //            c.Owner = dst;
            //        }
            //    } );
        }
    }
}
