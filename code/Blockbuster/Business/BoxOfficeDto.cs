//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : BoxOfficeDto.cs
// Description : Holds all the data that is returned by the BoxOffice service.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cingulariti.Blockbuster.Business
{
    [DataContract]
    public class BoxOfficeDto
    {
        [DataMember]
        public List<MovieDto> Movies { get; set; }

        [DataMember]
        public DateTime ResultsDate { get; set; }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
