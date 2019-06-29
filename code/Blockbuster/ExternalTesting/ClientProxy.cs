﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cingulariti.Blockbuster.Business
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BoxOfficeDto", Namespace = "http://schemas.datacontract.org/2004/07/Cingulariti.Blockbuster.Business")]
    public partial class BoxOfficeDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private Cingulariti.Blockbuster.Business.MovieDto[] MoviesField;

        private System.DateTime ResultsDateField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Cingulariti.Blockbuster.Business.MovieDto[] Movies
        {
            get
            {
                return this.MoviesField;
            }
            set
            {
                this.MoviesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ResultsDate
        {
            get
            {
                return this.ResultsDateField;
            }
            set
            {
                this.ResultsDateField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "MovieDto", Namespace = "http://schemas.datacontract.org/2004/07/Cingulariti.Blockbuster.Business")]
    public partial class MovieDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private decimal AverageField;

        private decimal DailyGrossField;

        private int DayNumberField;

        private double PercentChangeField;

        private int RankField;

        private string StudioField;

        private int TheatersField;

        private string TitleField;

        private decimal TotalGrossField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Average
        {
            get
            {
                return this.AverageField;
            }
            set
            {
                this.AverageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal DailyGross
        {
            get
            {
                return this.DailyGrossField;
            }
            set
            {
                this.DailyGrossField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DayNumber
        {
            get
            {
                return this.DayNumberField;
            }
            set
            {
                this.DayNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public double PercentChange
        {
            get
            {
                return this.PercentChangeField;
            }
            set
            {
                this.PercentChangeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rank
        {
            get
            {
                return this.RankField;
            }
            set
            {
                this.RankField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Studio
        {
            get
            {
                return this.StudioField;
            }
            set
            {
                this.StudioField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Theaters
        {
            get
            {
                return this.TheatersField;
            }
            set
            {
                this.TheatersField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this.TitleField;
            }
            set
            {
                this.TitleField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TotalGross
        {
            get
            {
                return this.TotalGrossField;
            }
            set
            {
                this.TotalGrossField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IBoxOffice")]
public interface IBoxOffice
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IBoxOffice/GetBoxOfficeResults", ReplyAction = "http://tempuri.org/IBoxOffice/GetBoxOfficeResultsResponse")]
    Cingulariti.Blockbuster.Business.BoxOfficeDto GetBoxOfficeResults(System.DateTime resultsDate);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IBoxOfficeChannel : IBoxOffice, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class BoxOfficeClient : System.ServiceModel.ClientBase<IBoxOffice>, IBoxOffice
{

    public BoxOfficeClient()
    {
    }

    public BoxOfficeClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    {
    }

    public BoxOfficeClient(string endpointConfigurationName, string remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public BoxOfficeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public BoxOfficeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    {
    }

    public Cingulariti.Blockbuster.Business.BoxOfficeDto GetBoxOfficeResults(System.DateTime resultsDate)
    {
        return base.Channel.GetBoxOfficeResults(resultsDate);
    }
}
