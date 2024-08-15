
var system = JSON.parse(localStorage.getItem("system") ?? '{}');
system.code = "__systemCode__";
localStorage.setItem("system", JSON.stringify(system));

localStorage.setItem("sga.baseUrl", "__sgaApiBaseUrl__");

localStorage.setItem("identity.settings", JSON.stringify({
  clientId: "__identityInternalClientId__",
  authority: "__identityInternalAuthority__",
  redirectUrl: "__identityInternalRedirectUrl__",
  scopes: JSON.stringify(JSON.parse(("__identityInternalScope__")?.replaceAll("'", '"'))),
  redirectAfterLogin: "__identityRedirectAfterLogin__",
  redirectAfterLogout: "__identityRedirectAfterLogout__",
  logoUrl: "__identityAuthLogoUrl__",
  backgroundUrl: "__identityAuthBackgroundUrl__",
  grandTypeRefresh: "__identityGrandTypeRefresh__",
  clientSecret: "__identityClientSecret__",
  baseUrl: "__identityBaseUrl__"
}));

localStorage.setItem("identity.external.settings", JSON.stringify({
  baseUrl: "__identityExternalBaseUrl__",
  clientId: "__identityExternalClientId__",
  clientSecret: "__identityExternalClientSecret__",
  grandType: "__identityExternalGrandType__",
  grandTypeRefresh: "__identityExternalGrandTypeRefresh__",
  identityScope: "__identityExternalScope__",
  securityMicroservice: "__identityExternalSecurityMicroservice__"
}));

localStorage.setItem("header.settings", JSON.stringify({
  title: "__headerTitle__",
  theme: "__headerTheme__",
  logoSrc: "__headerLogoUrl__",
  renderWhenUnauthenticated: false,
  unauthenticatedMenu: [],
  authenticatedUserOptions: []
}));

localStorage.setItem('styles', '__styleHeaders__');
