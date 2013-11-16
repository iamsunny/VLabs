// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

// dllmain.h : Declaration of module class.

class CLegacyCOMModule : public ATL::CAtlDllModuleT< CLegacyCOMModule >
{
public :
	DECLARE_LIBID(LIBID_LegacyCOMLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_LEGACYCOM, "{0AB61771-137A-4065-BD6C-AD3C47114011}")
};

extern class CLegacyCOMModule _AtlModule;
