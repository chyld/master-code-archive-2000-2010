//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : IBoxOffice.cs
// Description : Interface for BoxOffice service.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.ServiceModel;
using Cingulariti.ANDROMEDA.Business;

namespace Cingulariti.ANDROMEDA.BoxOffice
{
    [ServiceContract]
    public interface IBoxOffice
    {
        [OperationContract]
        BoxOfficeDto GetBoxOfficeResults(DateTime resultsDate);
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
