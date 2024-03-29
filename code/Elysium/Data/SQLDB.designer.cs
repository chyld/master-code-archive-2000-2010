﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chyld.Elysium.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="SQL2005_483471_triniti")]
	public partial class ElysiumDB : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCustomer(Customer instance);
    partial void UpdateCustomer(Customer instance);
    partial void DeleteCustomer(Customer instance);
    partial void InsertFacade(Facade instance);
    partial void UpdateFacade(Facade instance);
    partial void DeleteFacade(Facade instance);
    partial void InsertSection(Section instance);
    partial void UpdateSection(Section instance);
    partial void DeleteSection(Section instance);
    partial void InsertNotebook(Notebook instance);
    partial void UpdateNotebook(Notebook instance);
    partial void DeleteNotebook(Notebook instance);
    partial void InsertFont(Font instance);
    partial void UpdateFont(Font instance);
    partial void DeleteFont(Font instance);
    partial void InsertAgenda(Agenda instance);
    partial void UpdateAgenda(Agenda instance);
    partial void DeleteAgenda(Agenda instance);
    #endregion
		
		public ElysiumDB() : 
				base(global::Data.Properties.Settings.Default.SQL2005_483471_trinitiConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ElysiumDB(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ElysiumDB(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ElysiumDB(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ElysiumDB(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
		
		public System.Data.Linq.Table<Facade> Facades
		{
			get
			{
				return this.GetTable<Facade>();
			}
		}
		
		public System.Data.Linq.Table<Section> Sections
		{
			get
			{
				return this.GetTable<Section>();
			}
		}
		
		public System.Data.Linq.Table<Notebook> Notebooks
		{
			get
			{
				return this.GetTable<Notebook>();
			}
		}
		
		public System.Data.Linq.Table<Font> Fonts
		{
			get
			{
				return this.GetTable<Font>();
			}
		}
		
		public System.Data.Linq.Table<Agenda> Agendas
		{
			get
			{
				return this.GetTable<Agenda>();
			}
		}
	}
	
	[Table(Name="dbo.Elysium_User")]
	public partial class Customer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private string _Username;
		
		private string _Password;
		
		private EntitySet<Section> _Sections;
		
		private EntitySet<Notebook> _Notebooks;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    #endregion
		
		public Customer()
		{
			this._Sections = new EntitySet<Section>(new Action<Section>(this.attach_Sections), new Action<Section>(this.detach_Sections));
			this._Notebooks = new EntitySet<Notebook>(new Action<Notebook>(this.attach_Notebooks), new Action<Notebook>(this.detach_Notebooks));
			OnCreated();
		}
		
		[Column(Storage="_UserId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[Column(Storage="_Username", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[Column(Storage="_Password", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[Association(Name="Elysium_User_Elysium_Section", Storage="_Sections", OtherKey="UserId")]
		public EntitySet<Section> Sections
		{
			get
			{
				return this._Sections;
			}
			set
			{
				this._Sections.Assign(value);
			}
		}
		
		[Association(Name="Elysium_User_Elysium_Notebook", Storage="_Notebooks", OtherKey="UserId")]
		public EntitySet<Notebook> Notebooks
		{
			get
			{
				return this._Notebooks;
			}
			set
			{
				this._Notebooks.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Sections(Section entity)
		{
			this.SendPropertyChanging();
			entity.Customer = this;
		}
		
		private void detach_Sections(Section entity)
		{
			this.SendPropertyChanging();
			entity.Customer = null;
		}
		
		private void attach_Notebooks(Notebook entity)
		{
			this.SendPropertyChanging();
			entity.Customer = this;
		}
		
		private void detach_Notebooks(Notebook entity)
		{
			this.SendPropertyChanging();
			entity.Customer = null;
		}
	}
	
	[Table(Name="dbo.Elysium_Style")]
	public partial class Facade : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _StyleId;
		
		private int _FontId;
		
		private int _Size;
		
		private int _Spacing;
		
		private string _Color;
		
		private bool _IsBold;
		
		private bool _IsItalic;
		
		private bool _IsUnderline;
		
		private EntitySet<Section> _Sections;
		
		private EntityRef<Font> _Font;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnStyleIdChanging(int value);
    partial void OnStyleIdChanged();
    partial void OnFontIdChanging(int value);
    partial void OnFontIdChanged();
    partial void OnSizeChanging(int value);
    partial void OnSizeChanged();
    partial void OnSpacingChanging(int value);
    partial void OnSpacingChanged();
    partial void OnColorChanging(string value);
    partial void OnColorChanged();
    partial void OnIsBoldChanging(bool value);
    partial void OnIsBoldChanged();
    partial void OnIsItalicChanging(bool value);
    partial void OnIsItalicChanged();
    partial void OnIsUnderlineChanging(bool value);
    partial void OnIsUnderlineChanged();
    #endregion
		
		public Facade()
		{
			this._Sections = new EntitySet<Section>(new Action<Section>(this.attach_Sections), new Action<Section>(this.detach_Sections));
			this._Font = default(EntityRef<Font>);
			OnCreated();
		}
		
		[Column(Storage="_StyleId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int StyleId
		{
			get
			{
				return this._StyleId;
			}
			set
			{
				if ((this._StyleId != value))
				{
					this.OnStyleIdChanging(value);
					this.SendPropertyChanging();
					this._StyleId = value;
					this.SendPropertyChanged("StyleId");
					this.OnStyleIdChanged();
				}
			}
		}
		
		[Column(Storage="_FontId", DbType="Int NOT NULL")]
		public int FontId
		{
			get
			{
				return this._FontId;
			}
			set
			{
				if ((this._FontId != value))
				{
					if (this._Font.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFontIdChanging(value);
					this.SendPropertyChanging();
					this._FontId = value;
					this.SendPropertyChanged("FontId");
					this.OnFontIdChanged();
				}
			}
		}
		
		[Column(Storage="_Size", DbType="Int NOT NULL")]
		public int Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				if ((this._Size != value))
				{
					this.OnSizeChanging(value);
					this.SendPropertyChanging();
					this._Size = value;
					this.SendPropertyChanged("Size");
					this.OnSizeChanged();
				}
			}
		}
		
		[Column(Storage="_Spacing", DbType="Int NOT NULL")]
		public int Spacing
		{
			get
			{
				return this._Spacing;
			}
			set
			{
				if ((this._Spacing != value))
				{
					this.OnSpacingChanging(value);
					this.SendPropertyChanging();
					this._Spacing = value;
					this.SendPropertyChanged("Spacing");
					this.OnSpacingChanged();
				}
			}
		}
		
		[Column(Storage="_Color", DbType="NChar(6) NOT NULL", CanBeNull=false)]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this.OnColorChanging(value);
					this.SendPropertyChanging();
					this._Color = value;
					this.SendPropertyChanged("Color");
					this.OnColorChanged();
				}
			}
		}
		
		[Column(Storage="_IsBold", DbType="Bit NOT NULL")]
		public bool IsBold
		{
			get
			{
				return this._IsBold;
			}
			set
			{
				if ((this._IsBold != value))
				{
					this.OnIsBoldChanging(value);
					this.SendPropertyChanging();
					this._IsBold = value;
					this.SendPropertyChanged("IsBold");
					this.OnIsBoldChanged();
				}
			}
		}
		
		[Column(Storage="_IsItalic", DbType="Bit NOT NULL")]
		public bool IsItalic
		{
			get
			{
				return this._IsItalic;
			}
			set
			{
				if ((this._IsItalic != value))
				{
					this.OnIsItalicChanging(value);
					this.SendPropertyChanging();
					this._IsItalic = value;
					this.SendPropertyChanged("IsItalic");
					this.OnIsItalicChanged();
				}
			}
		}
		
		[Column(Storage="_IsUnderline", DbType="Bit NOT NULL")]
		public bool IsUnderline
		{
			get
			{
				return this._IsUnderline;
			}
			set
			{
				if ((this._IsUnderline != value))
				{
					this.OnIsUnderlineChanging(value);
					this.SendPropertyChanging();
					this._IsUnderline = value;
					this.SendPropertyChanged("IsUnderline");
					this.OnIsUnderlineChanged();
				}
			}
		}
		
		[Association(Name="Elysium_Style_Elysium_Section", Storage="_Sections", OtherKey="StyleId")]
		public EntitySet<Section> Sections
		{
			get
			{
				return this._Sections;
			}
			set
			{
				this._Sections.Assign(value);
			}
		}
		
		[Association(Name="Elysium_Font_Elysium_Style", Storage="_Font", ThisKey="FontId", IsForeignKey=true)]
		public Font Font
		{
			get
			{
				return this._Font.Entity;
			}
			set
			{
				Font previousValue = this._Font.Entity;
				if (((previousValue != value) 
							|| (this._Font.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Font.Entity = null;
						previousValue.Facades.Remove(this);
					}
					this._Font.Entity = value;
					if ((value != null))
					{
						value.Facades.Add(this);
						this._FontId = value.FontId;
					}
					else
					{
						this._FontId = default(int);
					}
					this.SendPropertyChanged("Font");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Sections(Section entity)
		{
			this.SendPropertyChanging();
			entity.Facade = this;
		}
		
		private void detach_Sections(Section entity)
		{
			this.SendPropertyChanging();
			entity.Facade = null;
		}
	}
	
	[Table(Name="dbo.Elysium_Section")]
	public partial class Section : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SectionId;
		
		private int _UserId;
		
		private int _StyleId;
		
		private string _Name;
		
		private EntitySet<Agenda> _Agendas;
		
		private EntityRef<Facade> _Facade;
		
		private EntityRef<Customer> _Customer;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSectionIdChanging(int value);
    partial void OnSectionIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnStyleIdChanging(int value);
    partial void OnStyleIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Section()
		{
			this._Agendas = new EntitySet<Agenda>(new Action<Agenda>(this.attach_Agendas), new Action<Agenda>(this.detach_Agendas));
			this._Facade = default(EntityRef<Facade>);
			this._Customer = default(EntityRef<Customer>);
			OnCreated();
		}
		
		[Column(Storage="_SectionId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int SectionId
		{
			get
			{
				return this._SectionId;
			}
			set
			{
				if ((this._SectionId != value))
				{
					this.OnSectionIdChanging(value);
					this.SendPropertyChanging();
					this._SectionId = value;
					this.SendPropertyChanged("SectionId");
					this.OnSectionIdChanged();
				}
			}
		}
		
		[Column(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._Customer.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[Column(Storage="_StyleId", DbType="Int NOT NULL")]
		public int StyleId
		{
			get
			{
				return this._StyleId;
			}
			set
			{
				if ((this._StyleId != value))
				{
					if (this._Facade.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnStyleIdChanging(value);
					this.SendPropertyChanging();
					this._StyleId = value;
					this.SendPropertyChanged("StyleId");
					this.OnStyleIdChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Association(Name="Elysium_Section_Elysium_Calendar", Storage="_Agendas", OtherKey="SectionId")]
		public EntitySet<Agenda> Agendas
		{
			get
			{
				return this._Agendas;
			}
			set
			{
				this._Agendas.Assign(value);
			}
		}
		
		[Association(Name="Elysium_Style_Elysium_Section", Storage="_Facade", ThisKey="StyleId", IsForeignKey=true)]
		public Facade Facade
		{
			get
			{
				return this._Facade.Entity;
			}
			set
			{
				Facade previousValue = this._Facade.Entity;
				if (((previousValue != value) 
							|| (this._Facade.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Facade.Entity = null;
						previousValue.Sections.Remove(this);
					}
					this._Facade.Entity = value;
					if ((value != null))
					{
						value.Sections.Add(this);
						this._StyleId = value.StyleId;
					}
					else
					{
						this._StyleId = default(int);
					}
					this.SendPropertyChanged("Facade");
				}
			}
		}
		
		[Association(Name="Elysium_User_Elysium_Section", Storage="_Customer", ThisKey="UserId", IsForeignKey=true)]
		public Customer Customer
		{
			get
			{
				return this._Customer.Entity;
			}
			set
			{
				Customer previousValue = this._Customer.Entity;
				if (((previousValue != value) 
							|| (this._Customer.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Customer.Entity = null;
						previousValue.Sections.Remove(this);
					}
					this._Customer.Entity = value;
					if ((value != null))
					{
						value.Sections.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("Customer");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Agendas(Agenda entity)
		{
			this.SendPropertyChanging();
			entity.Section = this;
		}
		
		private void detach_Agendas(Agenda entity)
		{
			this.SendPropertyChanging();
			entity.Section = null;
		}
	}
	
	[Table(Name="dbo.Elysium_Notebook")]
	public partial class Notebook : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _NotebookId;
		
		private int _UserId;
		
		private string _Name;
		
		private string _Value;
		
		private EntityRef<Customer> _Customer;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnNotebookIdChanging(int value);
    partial void OnNotebookIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnValueChanging(string value);
    partial void OnValueChanged();
    #endregion
		
		public Notebook()
		{
			this._Customer = default(EntityRef<Customer>);
			OnCreated();
		}
		
		[Column(Storage="_NotebookId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int NotebookId
		{
			get
			{
				return this._NotebookId;
			}
			set
			{
				if ((this._NotebookId != value))
				{
					this.OnNotebookIdChanging(value);
					this.SendPropertyChanging();
					this._NotebookId = value;
					this.SendPropertyChanged("NotebookId");
					this.OnNotebookIdChanged();
				}
			}
		}
		
		[Column(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._Customer.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Value", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._Value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		[Association(Name="Elysium_User_Elysium_Notebook", Storage="_Customer", ThisKey="UserId", IsForeignKey=true)]
		public Customer Customer
		{
			get
			{
				return this._Customer.Entity;
			}
			set
			{
				Customer previousValue = this._Customer.Entity;
				if (((previousValue != value) 
							|| (this._Customer.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Customer.Entity = null;
						previousValue.Notebooks.Remove(this);
					}
					this._Customer.Entity = value;
					if ((value != null))
					{
						value.Notebooks.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("Customer");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Elysium_Font")]
	public partial class Font : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _FontId;
		
		private string _Name;
		
		private EntitySet<Facade> _Facades;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFontIdChanging(int value);
    partial void OnFontIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Font()
		{
			this._Facades = new EntitySet<Facade>(new Action<Facade>(this.attach_Facades), new Action<Facade>(this.detach_Facades));
			OnCreated();
		}
		
		[Column(Storage="_FontId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int FontId
		{
			get
			{
				return this._FontId;
			}
			set
			{
				if ((this._FontId != value))
				{
					this.OnFontIdChanging(value);
					this.SendPropertyChanging();
					this._FontId = value;
					this.SendPropertyChanged("FontId");
					this.OnFontIdChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Association(Name="Elysium_Font_Elysium_Style", Storage="_Facades", OtherKey="FontId")]
		public EntitySet<Facade> Facades
		{
			get
			{
				return this._Facades;
			}
			set
			{
				this._Facades.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Facades(Facade entity)
		{
			this.SendPropertyChanging();
			entity.Font = this;
		}
		
		private void detach_Facades(Facade entity)
		{
			this.SendPropertyChanging();
			entity.Font = null;
		}
	}
	
	[Table(Name="dbo.Elysium_Calendar")]
	public partial class Agenda : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CalendarId;
		
		private int _SectionId;
		
		private System.DateTime _Day;
		
		private string _Value;
		
		private EntityRef<Section> _Section;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCalendarIdChanging(int value);
    partial void OnCalendarIdChanged();
    partial void OnSectionIdChanging(int value);
    partial void OnSectionIdChanged();
    partial void OnDayChanging(System.DateTime value);
    partial void OnDayChanged();
    partial void OnValueChanging(string value);
    partial void OnValueChanged();
    #endregion
		
		public Agenda()
		{
			this._Section = default(EntityRef<Section>);
			OnCreated();
		}
		
		[Column(Storage="_CalendarId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CalendarId
		{
			get
			{
				return this._CalendarId;
			}
			set
			{
				if ((this._CalendarId != value))
				{
					this.OnCalendarIdChanging(value);
					this.SendPropertyChanging();
					this._CalendarId = value;
					this.SendPropertyChanged("CalendarId");
					this.OnCalendarIdChanged();
				}
			}
		}
		
		[Column(Storage="_SectionId", DbType="Int NOT NULL")]
		public int SectionId
		{
			get
			{
				return this._SectionId;
			}
			set
			{
				if ((this._SectionId != value))
				{
					if (this._Section.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSectionIdChanging(value);
					this.SendPropertyChanging();
					this._SectionId = value;
					this.SendPropertyChanged("SectionId");
					this.OnSectionIdChanged();
				}
			}
		}
		
		[Column(Storage="_Day", DbType="DateTime NOT NULL")]
		public System.DateTime Day
		{
			get
			{
				return this._Day;
			}
			set
			{
				if ((this._Day != value))
				{
					this.OnDayChanging(value);
					this.SendPropertyChanging();
					this._Day = value;
					this.SendPropertyChanged("Day");
					this.OnDayChanged();
				}
			}
		}
		
		[Column(Storage="_Value", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._Value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		[Association(Name="Elysium_Section_Elysium_Calendar", Storage="_Section", ThisKey="SectionId", IsForeignKey=true)]
		public Section Section
		{
			get
			{
				return this._Section.Entity;
			}
			set
			{
				Section previousValue = this._Section.Entity;
				if (((previousValue != value) 
							|| (this._Section.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Section.Entity = null;
						previousValue.Agendas.Remove(this);
					}
					this._Section.Entity = value;
					if ((value != null))
					{
						value.Agendas.Add(this);
						this._SectionId = value.SectionId;
					}
					else
					{
						this._SectionId = default(int);
					}
					this.SendPropertyChanged("Section");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
