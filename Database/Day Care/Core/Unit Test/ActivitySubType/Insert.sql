/******************************************************************************
**		Name: ActivitySubType
*******************************************************************************/

EXEC dbo.ActivitySubTypeInsert @ActivitySubTypeId = 61	, @ActivityTypeId = 74,	@Name = 'Jassi'	    ,	@Description = 'Europe'		,	@SortOrder = 64 , @AuditId=5
EXEC dbo.ActivitySubTypeInsert @ActivitySubTypeId = 62 , @ActivityTypeId = 74,	@Name = 'Rome'	    ,	@Description = 'NewYork'	,   @SortOrder = 65 , @AuditId=5
EXEC dbo.ActivitySubTypeInsert @ActivitySubTypeId = 63	,@ActivityTypeId = 74,	@Name = 'Jolite'	,	@Description = 'Austrila'	,	@SortOrder = 66 , @AuditId=5

