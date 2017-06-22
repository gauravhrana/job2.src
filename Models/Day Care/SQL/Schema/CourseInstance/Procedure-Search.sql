IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseInstanceSearch') 
BEGIN
	DROP Procedure CourseInstanceSearch
END
GO

CREATE Procedure dbo.CourseInstanceSearch
(
		@CourseInstanceId				INT		= NULL
	,	@Name				VARCHAR(500) = NULL
	,	@CourseId				INT		= NULL
	,	@DepartmentId				INT		= NULL
	,	@TeacherId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'CourseInstance'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN


	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @Name  IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@Name = '%'
	END

	SELECT 
			a. CourseInstanceId
		,	a. Name
		,	a. CourseId
		,	Course.Name AS Course
		,	a. DepartmentId
		,	Department.Name AS Department
		,	a. TeacherId
		,	Teacher.Name AS Teacher
		,	a. StartTime
		,	a. EndTime
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.CourseInstance a
	INNER JOIN Course ON Course.CourseId = a.CourseId
	INNER JOIN Department ON Department.DepartmentId = a.DepartmentId
	INNER JOIN Teacher ON Teacher.TeacherId = a.TeacherId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.CourseInstanceId = ISNULL(@CourseInstanceId, a.CourseInstanceId)
	AND		a.Name	LIKE	@Name + '%'
	AND		a.CourseId = ISNULL(@CourseId, a.CourseId)
	AND		a.DepartmentId = ISNULL(@DepartmentId, a.DepartmentId)
	AND		a.TeacherId = ISNULL(@TeacherId, a.TeacherId)
	ORDER BY	a.CourseInstanceId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE CourseInstanceId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.CourseInstanceId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.CourseInstanceId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.CourseInstanceId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.CourseInstanceId=b.CourseInstanceId
		ORDER BY	a.CourseInstanceId
	END
	ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)

		SELECT	a.*
			,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a	
		ORDER BY	a.CourseInstanceId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @CourseInstanceId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
