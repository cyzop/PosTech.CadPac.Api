﻿using PosTech.CadPac.Domain.Shared.Entities;

namespace PosTech.CadPac.Domain.Shared.Converter
{
    public interface IConverter<in I, out O> where I : Entity where O : Entity
    {
        public O Convert(I origing);
    }
}
