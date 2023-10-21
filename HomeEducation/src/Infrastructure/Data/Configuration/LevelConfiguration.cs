using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeEducation.Domain.Entities;
using HomeEducation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEducation.Infrastructure.Data.Configuration;
internal class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        var levels = new List<Level> {
            new Level { Id = "1Primary", TitleEn = "First", TitleAr = "الاول", Phase = StudyPhase.Primary },
            new Level { Id = "2Primary", TitleEn = "Second", TitleAr = "الثاني", Phase = StudyPhase.Primary },
            new Level { Id = "3Primary", TitleEn = "Third", TitleAr = "الثالث", Phase = StudyPhase.Primary },
            new Level { Id = "4Primary", TitleEn = "Fourth", TitleAr = "الرابع", Phase = StudyPhase.Primary },
            new Level { Id = "5Primary", TitleEn = "Fifth", TitleAr = "الخامس", Phase = StudyPhase.Primary },
            new Level { Id = "6Primary", TitleEn = "Sixth", TitleAr = "السادس", Phase = StudyPhase.Primary },

            new Level { Id = "1Prepare", TitleEn = "First", TitleAr = "الاول", Phase = StudyPhase.Preparatory },
            new Level { Id = "2Prepare", TitleEn = "Second", TitleAr = "الثاني", Phase = StudyPhase.Preparatory },
            new Level { Id = "3Prepare", TitleEn = "Third", TitleAr = "الثالث", Phase = StudyPhase.Preparatory },

            new Level { Id = "1Secondary", TitleEn = "First", TitleAr = "الاول", Phase = StudyPhase.Secondary },
            new Level { Id = "2Secondary", TitleEn = "Second", TitleAr = "الثاني", Phase = StudyPhase.Secondary },
            new Level { Id = "3Secondary", TitleEn = "Third", TitleAr = "الثالث", Phase = StudyPhase.Secondary }
    };
        builder.HasData(levels);
    }
}
