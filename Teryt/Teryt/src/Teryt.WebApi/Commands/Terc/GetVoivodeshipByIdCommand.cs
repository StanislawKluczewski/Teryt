﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Teryt.WebApi.Data;
using Teryt.WebApi.DTO.Response;

namespace Teryt.WebApi.Commands.Terc
{
    public class GetVoivodeshipByIdCommand : IRequest<IEnumerable<TERCDto>>
    {
        public int Id { get; set; }
        public class GetVoivodeshipByIdCommandHandler : IRequestHandler<GetVoivodeshipByIdCommand, IEnumerable<TERCDto>>
        {
            private readonly DataContext context;

            public GetVoivodeshipByIdCommandHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<TERCDto>> Handle(GetVoivodeshipByIdCommand request, CancellationToken cancellationToken)
            {
                var result = from v in context.TERCs
                             where (v.PowiatId == null && v.GminaId == null && v.RodzGminaId == null)
                             && v.WojewodztwoId == request.Id
                             select new TERCDto
                             {
                                 Nazwa = v.Nazwa,
                                 NazwaTerytorialna = v.NazwaTerytorialna,
                                 WojewodztwoId = request.Id,
                                 StanNa = v.StanNa
                             };
                return result;
            }
        }
    }
}