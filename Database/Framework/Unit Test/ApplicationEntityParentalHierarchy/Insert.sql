﻿/******************************************************************************
**		Name: ApplicationEntityParentalHierarchy
*******************************************************************************/

EXEC dbo.ApplicationEntityParentalHierarchyInsert @ApplicationEntityParentalHierarchyId = -131	, @Name = 'Resume'	  , @Description = 'Received'		, @SortOrder = 4	, @AuditId = 400		, @AuditDate ='12/13/2011'
EXEC dbo.ApplicationEntityParentalHierarchyInsert @ApplicationEntityParentalHierarchyId = -132	, @Name = 'Joining'   , @Description = 'NotReceived'    , @SortOrder = 7    , @AuditId = 400     , @AuditDate ='12/15/2011'
EXEC dbo.ApplicationEntityParentalHierarchyInsert @ApplicationEntityParentalHierarchyId = -331	, @Name = 'Payment'   , @Description = 'Accepted'       , @SortOrder = 3    , @AuditId = 400     , @AuditDate ='12/17/2011'

