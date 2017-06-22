	--
DECLARE @DBName			AS	VARCHAR(50)
DECLARE @MinValue		AS	INT	
DECLARE @MaxValue		AS INT
DECLARE @sql			AS  NVARCHAR(512)	
	
SELECT	@DBName				= a.ConnectionKeyName
FROM	Configuration.dbo.SetUpConfiguration a
WHERE	a.EntityName		= '@EntityName@'

-- Get Min Id
SET		@sql =  N'SELECT @MinValue = MIN(@EntityName@Id) '
			+	' FROM ' + @DBName + '.dbo.@EntityName@ '

EXEC	sp_executesql 
		@query = @sql, 
		@params = N'@MinValue INT OUTPUT', 
		@MinValue = @MinValue OUTPUT 

-- Get Max Id
SET		@sql =  N'SELECT @MaxValue = MAX(@EntityName@Id) '
			+	' FROM ' + @DBName + '.dbo.@EntityName@ '

EXEC	sp_executesql 
		@query = @sql, 
		@params = N'@MaxValue INT OUTPUT', 
		@MaxValue = @MaxValue OUTPUT 

DECLARE @SuperKeyId		INT

EXEC	CommonServices.dbo.SuperKeyInsert
		@SuperKeyId				=	@SuperKeyId			OUTPUT	
,		@ApplicationId			=	@ApplicationId@	
,		@Name					=	@Name@
,		@Description			=   @Description@
,		@SortOrder				=	1
,		@SystemEntityTypeId		=	@SystemEntityTypeId@
,		@ExpirationDate			=	@ExpirationDate@
,		@AuditId				=	@AuditId@	

EXEC	CommonServices.dbo.SuperKeyDetailInsert
		@ApplicationId		=	@ApplicationId@
	,	@EntityKey			=	@MinValue
	,	@SuperKeyId			=	@SuperKeyId
	,	@AuditId			=   @AuditId@

IF	@MaxValue <> @MinValue
	BEGIN	

		EXEC	CommonServices.dbo.SuperKeyDetailInsert
				@ApplicationId		=	@ApplicationId@
			,	@EntityKey			=	@MaxValue
			,	@SuperKeyId			=	@SuperKeyId
			,	@AuditId			=   @AuditId@

	END
	
SELECT @SuperKeyId