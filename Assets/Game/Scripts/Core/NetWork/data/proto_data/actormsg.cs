//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: proto/actormsg.proto
namespace proto.actormsg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_RoleInfoRequest")]
  public partial class ST_RoleInfoRequest : global::ProtoBuf.IExtensible
  {
    public ST_RoleInfoRequest() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_RoleInfoResponse")]
  public partial class ST_RoleInfoResponse : global::ProtoBuf.IExtensible
  {
    public ST_RoleInfoResponse() {}
    
    private string _nickname = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"nickname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nickname
    {
      get { return _nickname; }
      set { _nickname = value; }
    }
    private string _headicon = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"headicon", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string headicon
    {
      get { return _headicon; }
      set { _headicon = value; }
    }
    private int _body = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"body", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int body
    {
      get { return _body; }
      set { _body = value; }
    }
    private string _token = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"token", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string token
    {
      get { return _token; }
      set { _token = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_PropChgRequest")]
  public partial class ST_PropChgRequest : global::ProtoBuf.IExtensible
  {
    public ST_PropChgRequest() {}
    
    private int _ptype;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"ptype", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int ptype
    {
      get { return _ptype; }
      set { _ptype = value; }
    }
    private string _context;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"context", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string context
    {
      get { return _context; }
      set { _context = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_PropChgResponse")]
  public partial class ST_PropChgResponse : global::ProtoBuf.IExtensible
  {
    public ST_PropChgResponse() {}
    
    private string _result;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_PlayerPropRequest")]
  public partial class ST_PlayerPropRequest : global::ProtoBuf.IExtensible
  {
    public ST_PlayerPropRequest() {}
    
    private readonly global::System.Collections.Generic.List<long> _uids = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"uids", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<long> uids
    {
      get { return _uids; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_PlayerProp")]
  public partial class ST_PlayerProp : global::ProtoBuf.IExtensible
  {
    public ST_PlayerProp() {}
    
    private long _uid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"uid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long uid
    {
      get { return _uid; }
      set { _uid = value; }
    }
    private string _nickname = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"nickname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nickname
    {
      get { return _nickname; }
      set { _nickname = value; }
    }
    private string _headicon = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"headicon", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string headicon
    {
      get { return _headicon; }
      set { _headicon = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ST_PlayerPropResponse")]
  public partial class ST_PlayerPropResponse : global::ProtoBuf.IExtensible
  {
    public ST_PlayerPropResponse() {}
    
    private readonly global::System.Collections.Generic.List<ST_PlayerProp> _proplist = new global::System.Collections.Generic.List<ST_PlayerProp>();
    [global::ProtoBuf.ProtoMember(1, Name=@"proplist", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ST_PlayerProp> proplist
    {
      get { return _proplist; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}