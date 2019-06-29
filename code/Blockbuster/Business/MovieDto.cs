//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : MovieDto.cs
// Description : Holds the statistical data for a single movie.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.Runtime.Serialization;

namespace Cingulariti.Blockbuster.Business
{
    [DataContract]
    public class MovieDto
    {
        [DataMember]
        public Int32 Rank { get; set; }

        [DataMember]
        public String Title { get; set; }
        
        [DataMember]
        public String Studio { get; set; }
        
        [DataMember]
        public Int32 Theaters { get; set; }
        
        [DataMember]
        public Decimal DailyGross { get; set; }
        
        [DataMember]
        public Double PercentChange { get; set; }
        
        [DataMember]
        public Decimal Average { get; set; }
        
        [DataMember]
        public Decimal TotalGross { get; set; }
        
        [DataMember]
        public Int32 DayNumber { get; set; }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
