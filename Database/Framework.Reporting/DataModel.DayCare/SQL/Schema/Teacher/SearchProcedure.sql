IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TeacherSearch') 
	BEGIN
	DROP Procedure TeacherSearch
END
GO

CREATE Procedure dbo.TeacherSearch
(
		@TeacherId				INT							= NULL
	,	@ApplicationId					INT							= NULL
	,	@Name							VARCHAR(50)					= NULL
	,	@Description					VARCHAR(500)				= NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'Teacher'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN

		SET  NOCOUNT ON

		IF @AddTraceInfo = 1 
		BEGIN
			-- TRACE --
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		=  'TeacherId'  + ', ' + 'Name' + ', ' + 'Description'
			SET @InputValuesLocal			=  CAST(@TeacherId AS VARCHAR(50)) + ',' + ISNULL(@Name, 'NULL') + ',' + ISNULL(@Description, 'NULL')

			EXEC dbo.StoredProcedureLogInsert
					@Name					= 'dbo.TeacherSearch'
				,	@InputParameters		= @InputParametersLocal
				,	@InputValues			= @InputValuesLocal	
				-- TRACE --		
		END	

		DECLARE @SystemEntityTypeId AS INT
		SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

		--if blank, then assume search on all possiblities ('%')
		IF  @Name IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
		--if blank, then assume search on all possiblities ('%')
		IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
		BEGIN
			SET	@Description = '%'
		END


		SELECT 
				TeacherId
			,	ApplicationId
			,	Name
			,	Description
			,	SortOrder
		INTO		#TempMain
		FROM		dbo.Teacher a
		WHERE  a.Name LIKE @Name  + '%'
		AND  a.[Description]	LIKE @Description  + '%'
		AND	a.TeacherId = ISNULL(@TeacherId, a.TeacherId)
		AND	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
		ORDER BY	a.SortOrder		 ASC
		,			a.Name			 ASC
		,			a.TeacherId ASC

		IF	@ApplicationMode = 1 
		BEGIN
			DELETE FROM #TempMain
			WHERE TeacherId < 0
		END

		IF @ReturnAuditInfo = 1
		BEGIN
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey
					   ,MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
			INTO		#HistortyInfo
			FROM 		#TempMain a	
			INNER JOIN	CommonServices.dbo.AuditHistory c
				ON	c.EntityKey			= a.TeacherId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	

			-- Get Audit Date and CreatedByPersonId for given records
			SELECT
						a.TeacherId
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId
					, 	c.CreatedDate					AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
					,	d.Name							AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo							b
				ON	b.EntityKey			= a.TeacherId
			INNER JOIN	CommonServices.dbo.AuditHistory			c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction			d	
				ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e
				ON	c.CreatedByPersonId	= e.ApplicationUserId

			-- Show full details
			SELECT	a.*
				,	b.UpdatedDate
				,	b.UpdatedBy
				,	b.LastAction
			FROM	#TempMain		a
			LEFT JOIN	#HistortyInfoDetails	b
				ON	a.TeacherId=b.TeacherId
			ORDER BY	a.SortOrder				ASC
				,		a.TeacherId

		END
		ELSE
		BEGIN
			DECLARE @StaticUpdatedDate AS DATETIME
			SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)
			SELECT 	a.*
			,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
			FROM	#TempMain a	
			ORDER BY	a.SortOrder		ASC
					,	a.TeacherId
		END

		IF @AddAuditInfo = 1 
		BEGIN
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert 
					@SystemEntityType	= @SystemEntityType
				,	@EntityKey			= @TeacherId
				,	@AuditAction		= 'Search'
				,	@CreatedDate		= @AuditDate
				,	@CreatedByPersonId	= @AuditId
		END

END
GO
