IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RegistrationSearch') 
BEGIN
	DROP Procedure RegistrationSearch
END
GO

CREATE Procedure dbo.RegistrationSearch
(
		@RegistrationId				INT		= NULL
	,	@CourseId				INT		= NULL
	,	@StudentId				INT		= NULL
	,	@FromSearchEnrollmentDate				DATETIME = NULL
	,	@ToSearchEnrollmentDate				DATETIME = NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'Registration'
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


	SELECT 
			a. RegistrationId
		,	a. CourseId
		,	Course.Name AS Course
		,	a. StudentId
		,	Student.Name AS Student
		,	a. EnrollmentDate
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.Registration a
	INNER JOIN Course ON Course.CourseId = a.CourseId
	INNER JOIN Student ON Student.StudentId = a.StudentId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.RegistrationId = ISNULL(@RegistrationId, a.RegistrationId)
	AND		a.CourseId = ISNULL(@CourseId, a.CourseId)
	AND		a.StudentId = ISNULL(@StudentId, a.StudentId)
	AND		a.EnrollmentDate BETWEEN COALESCE(@FromSearchEnrollmentDate, a.EnrollmentDate) 	AND	 COALESCE(@ToSearchEnrollmentDate, a.EnrollmentDate)
	ORDER BY	a.RegistrationId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE RegistrationId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.RegistrationId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.RegistrationId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.RegistrationId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.RegistrationId=b.RegistrationId
		ORDER BY	a.RegistrationId
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
		ORDER BY	a.RegistrationId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @RegistrationId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
