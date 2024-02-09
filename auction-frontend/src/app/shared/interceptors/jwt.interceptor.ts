import { HttpInterceptorFn } from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const authReq = req.clone({
    headers: req.headers.set('Authorization', 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Ijk4YThhOTRiLTc2ZDUtNGFmZS04Y2Q2LWEzNWZiNTYzNzNjZSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFydGVtQGVtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBcnRlbSIsImV4cCI6MTcwNzc4MjYxMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE0NiIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQyMDAifQ.t7Wm-mB3Yj2aPh-zhSCcfAuDy4OCxscsKqMd6CYwFxA')
  })

  return next(authReq);
};
